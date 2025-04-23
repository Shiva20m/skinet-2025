import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { SnackbarService } from '../services/snackbar.service';

// interceptor to catch the error return from the api side
export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const snackBar = inject(SnackbarService);

  
  return next(req).pipe(
    catchError((err:HttpErrorResponse)=>{
      if(err.status===400)
      {
        if(err.error.errors)
        {
          const modelSateError = [];
          for(const key in err.error.errors)
          {
            if(err.error.errors[key])
            {
              modelSateError.push(err.error.errors[key]);
            }
          }
          throw modelSateError.flat();
        }
        else{
          snackBar.error(err.error.title || err.error);
        }
      }
      if(err.status===401)
      {
        snackBar.error(err.error.title || err.error);
      }
      if(err.status===404)
      {
        router.navigateByUrl('/not-found');
      }
      if(err.status===500)
      {
        // pass the error details into the component severor error
        const navigationExtras: NavigationExtras= {state: {error: err.error}};
        router.navigateByUrl('/server-error', navigationExtras);
      }
      return throwError(()=>err);
    })
  )
};
