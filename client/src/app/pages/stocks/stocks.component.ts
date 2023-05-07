import { Component } from '@angular/core';
import { StockInfo } from 'src/app/models/stockInfo';
import { StockDBService } from 'src/services/stockDB.service';

@Component({
  selector: 'app-stocks',
  templateUrl: './stocks.component.html',
  styleUrls: ['./stocks.component.css']
})
export class StocksComponent {
    
  constructor(private stockDBService: StockDBService) {}

  stockList: StockInfo[] = [];

  ngOnInit() {
    this.stockDBService.getAll().subscribe(data => {
      this.stockList = data;
    })
  }
}
