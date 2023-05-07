import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { StockInfo } from 'src/app/models/stockInfo';
import { StockService } from 'src/services/stock.service';
import { UserService } from 'src/services/user.service';


const ELEMENT_DATA: any[] = [];
@Component({
  selector: 'app-stock-search',
  templateUrl: './stock-search.component.html',
  styleUrls: ['./stock-search.component.css'],
}) 

export class StockSearchComponent {
  constructor(private stockService: StockService, private userService: UserService) {}

  public stocksFound: StockInfo[] = [];
  public searchValue: string = "";


  public search(searchValue: string ) {
    this.stockService.autoSearch(searchValue).subscribe((data: StockInfo[]) => {
      
      this.stocksFound = data;
    });
  }
}
