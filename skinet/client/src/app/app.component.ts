import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layouts/header/header.component";
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common'; 
import { Product } from './shared/models/product';
import { Pagination } from './shared/models/pagination';
import { Envelope } from './shared/models/envelope';
import { ShopService } from './core/services/shop.service';
import { ShopComponent } from "./features/shop/shop.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, CommonModule, ShopComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Skinet';
  
}
