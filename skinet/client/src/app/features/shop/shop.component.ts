import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';
import { Envelope } from '../../shared/models/envelope';
import { Pagination } from '../../shared/models/pagination';
import {MatCard} from '@angular/material/card'
import { MatDialog } from '@angular/material/dialog';
import { ProductItemComponent } from './product-item/product-item.component';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { ShopPramas } from '../../shared/models/ShopPramas';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-shop',
  standalone:true,
  imports: [ProductItemComponent, MatButton, MatCard, MatIcon, MatMenu, MatSelectionList, MatListOption, MatMenuTrigger, MatPaginatorModule, FormsModule],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {

  // move the product logic from app.component.ts
  // baseUrl = 'https://localhost:5001/api/';
  // private http = inject(HttpClient);
  // products: any[]= [];
  products?: Pagination<Product>;
  // what brand and types are selected by filter
  // selectedBrands:string[]=[];
  // selectedTypes:string[]=[];
  // selectedSort:string='';
  sortOptions=[
    {name: 'Price Low-High', value:'priceAsc'},
    {name: 'Price High-Low', value:'priceDesc'},

  ] 
  shopParams = new ShopPramas();
  pageSizeOptions = [5,10,15,20];

  // inject the shopservices
  private shopServices = inject(ShopService);
  private dialogService = inject(MatDialog);
  
  ngOnInit(): void {
    // this.http.get<Envelope<Pagination<Product>>>(this.baseUrl + 'products').subscribe({
    //   this.shopServices.getProducts().subscribe({
    //   next: (response:Envelope<Pagination<Product>>):void =>
    //   {
    //     console.log(response);
    //     this.products = response.value.data;
    //   },
    //   error:error=> console.log(error)
    // })
    this.intializeShop();
  }

  intializeShop()
  {
    this.shopServices.getBrands();
    this.shopServices.getTypes();
    // this.shopServices.getProducts().subscribe({
    //   next: (response:Envelope<Pagination<Product>>):void =>
    //   {
    //     console.log(response);
    //     this.products = response.value.data;
    //   },
    //   error:error=> console.log(error)
    // })
    this.getProducts();
  }

// apply sort functionality

getProducts()
{
  this.shopServices.getProducts(this.shopParams).subscribe({
    next:response=>this.products=response.value,
    error:error=> console.error(error)
  })
}

onSearchChange()
{
  this.shopParams.pageNumber=1;
  this.getProducts();
}

handlePageEvent(event:PageEvent)
{
  this.shopParams.pageNumber = event.pageIndex+1;
  this.shopParams.pageSize = event.pageSize;
  this.getProducts();
}
onSortChange(event:MatSelectionListChange)
{
  const selectedOption = event.options[0];
  if(selectedOption)
  {
    this.shopParams.sort =  selectedOption.value;
    // change
    this.shopParams.pageNumber=1;
    this.getProducts();
  }
}
// applying filter method using dialog
  openFilterDialog()
  {
    const dialogRef = this.dialogService.open(FiltersDialogComponent, {
      minWidth:'500px',
      data:{
        selectedBrands : this.shopParams.brands,
        selectedTypes: this.shopParams.types
      }
    })
    dialogRef.afterClosed().subscribe({
      next:result=>{
        if(result)
        {
          console.log("selected brand and type after selection",result);
          this.shopParams.brands=result.selectedBrands;
          this.shopParams.types=result.selectedTypes;
          // change
          this.shopParams.pageNumber=1;
          // apply the filter by calling service method
          this.getProducts();
        }
      }
    })
  }
}
