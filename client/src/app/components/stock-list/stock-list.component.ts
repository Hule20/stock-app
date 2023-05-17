import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Observable, map } from 'rxjs';
import { Stock } from 'src/app/models/stock';
import { StockInfo } from 'src/app/models/stockInfo';
import { StockDBService } from 'src/services/stockDB.service';

export interface StockDto {
  id: string;
  ticker: string;
  company: string;
}

@Component({
  selector: 'app-stock-list',
  templateUrl: './stock-list.component.html',
  styleUrls: ['./stock-list.component.css'],
})
export class StockListComponent {
  constructor(private stockDBService: StockDBService) {}

  public dataSource: StockDto[] = [];
  displayedColumns: string[] = ['ID', 'ticker', 'company', 'actions'];

  ngOnInit() {
    this.stockDBService.getAll().subscribe((resp) => {
      this.dataSource = resp;
    });
  }
}
