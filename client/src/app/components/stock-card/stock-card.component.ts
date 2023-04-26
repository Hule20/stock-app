import { Component, Input } from '@angular/core';
import { Stock } from 'src/app/models/stock';
import { StockService } from 'src/services/stock.service';

@Component({
  selector: 'app-stock-card',
  templateUrl: './stock-card.component.html',
  styleUrls: ['./stock-card.component.css']
})

export class StockCardComponent {

  public stock?: Stock;
  
  constructor(private stockService: StockService) { }

  ngOnInit() {
    // this.stockService.getSingleStock().subscribe({
    //   next: (data) => {
    //     this.stock = data;
    //   }
    // });
  }
}
