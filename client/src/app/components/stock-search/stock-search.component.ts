import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { StockService } from 'src/services/stock.service';

@Component({
  selector: 'app-stock-search',
  templateUrl: './stock-search.component.html',
  styleUrls: ['./stock-search.component.css'],
})
export class StockSearchComponent {
  constructor(private stockService: StockService) {}

  public stocksFound: string[] = [];
  public searchValue: string = "";

  ngOnInit() {
    
  }

  public search(searchValue: string){
    this.stockService.autoSearch(searchValue).subscribe((data: string[]) => {
      this.stocksFound = data;
    });
  }
}
