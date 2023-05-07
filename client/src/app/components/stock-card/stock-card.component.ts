import { Component, Input } from '@angular/core';
import { Stock } from 'src/app/models/stock';
import { StockInfo } from 'src/app/models/stockInfo';
import { StockDBService } from 'src/services/stockDB.service';

@Component({
  selector: 'app-stock-card',
  templateUrl: './stock-card.component.html',
  styleUrls: ['./stock-card.component.css']
})

export class StockCardComponent {
  @Input() stock!: StockInfo;
  
  constructor(private stockDBService: StockDBService) { }

  public addStockToUser(){
    return this.stockDBService.add(this.stock, '1').subscribe({
      next: (res) => console.log("added stock to this user's list"),
      error: (err) => console.log("error while adding a stock to user's list" + err)
    })
  }
}
