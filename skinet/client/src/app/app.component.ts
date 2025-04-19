import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layouts/header/header.component";
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common'; 
import { Product } from './shared/models/product';
import { Pagination } from './shared/models/pagination';
import { Envelope } from './shared/models/envelope';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent,CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  
  title = 'Skinet';
  // get the data from the server
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  // intialise an array to store the response
  // products: any[]= [];
  products: Product[]=[];
  ngOnInit(): void {
    this.http.get<Envelope<Pagination<Product>>>(this.baseUrl + 'products').subscribe({
      next: (response:Envelope<Pagination<Product>>):void =>
      {
        console.log(response);
        this.products = response.value.data;
        console.log(this.products);
      },
      error:error=> console.log(error),
      complete:() =>console.log("COMPLETE THE API FETCH")
    })
  }
}
