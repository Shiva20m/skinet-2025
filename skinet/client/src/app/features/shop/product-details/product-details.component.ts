import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../../core/services/shop.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../../shared/models/product';
import { CurrencyPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDivider } from '@angular/material/divider';

@Component({
  selector: 'app-product-details',
  standalone:true,
  imports: [CurrencyPipe, MatButton, MatIcon, MatFormFieldModule, MatInputModule, MatLabel, MatDivider],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {
  private shopServices = inject(ShopService);
  private activatedRoute = inject(ActivatedRoute);
  product?:Product;

  ngOnInit(): void {
      this.loadProduct();
  }

  loadProduct()
  {
    // give the id of the product as we click on the product via url
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if(!id) return;
    this.shopServices.getProduct(+id).subscribe({
      next:product=>this.product = product,
      error:error=>console.log(error)
    })
  }

}
