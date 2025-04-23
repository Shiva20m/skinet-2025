import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-test-error',
  imports: [MatButton],
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.scss'
})
export class TestErrorComponent {
  private baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  validationError?:string[];

  get404Error():void{
    this.http.get(this.baseUrl + 'buggy/notfound').subscribe({
      next:response=>console.log(response),
      error:error=>console.log(error)
    })
  }

  get400Error():void{
    this.http.get(this.baseUrl + 'buggy/badrequest').subscribe({
      next:response=>console.log(response),
      error:error=>console.log(error)
    })
  }

  get401Error():void{
    this.http.get(this.baseUrl + 'buggy/unauthorized').subscribe({
      next:response=>console.log(response),
      error:error=>console.log(error)
    })
  }

  get500Error():void{
    this.http.get(this.baseUrl + 'buggy/internalerror').subscribe({
      next:response=>console.log(response),
      error:error=>console.log(error)
    })
  }

  get400ValidatioError():void{
    this.http.post(this.baseUrl + 'buggy/validationerror', {}).subscribe({
      next:response=>console.log(response),
      error:error=>this.validationError=error
    })
  }

}
