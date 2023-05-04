import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { StockInfo } from 'src/app/models/stockInfo';
import { StockService } from 'src/services/stock.service';

@Component({
  selector: 'app-stock-search',
  templateUrl: './stock-search.component.html',
  styleUrls: ['./stock-search.component.css'],
})
export class StockSearchComponent {
  constructor(private stockService: StockService) {}

  public stocksFound: StockInfo[] = [];
  public searchValue: string = "";

  ngOnInit() {
    
  }

  public search(searchValue: string){
    this.stockService.autoSearch(searchValue).subscribe((data: StockInfo[]) => {
      
      this.stocksFound = data;
    });
  }

  public addStock(stockToAdd: StockInfo){
    this.stockService.add(stockToAdd).subscribe({
      next: () => console.log("Succesfully added"),
      error: (err) => console.log(err)
    })
  }
}
