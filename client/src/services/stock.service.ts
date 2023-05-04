import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Stock } from 'src/app/models/stock';
import { StockInfo } from 'src/app/models/stockInfo';
import { map } from 'rxjs/operators';
import { API_KEY_ALPHA_VANTAGE } from 'src/config';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class StockService {
  private baseUrl = 'https://www.alphavantage.co/query';
  private params = {
    function: 'TIME_SERIES_DAILY_ADJUSTED',
    symbol: 'AAPL',
    interval: '1min',
    apikey: API_KEY_ALPHA_VANTAGE,
  };

  constructor(private http: HttpClient) {}

  public getSingleStock() {
    const apiUrl = `${this.baseUrl}?function=${this.params.function}&symbol=${this.params.symbol}&interval=${this.params.interval}&apikey=${this.params.apikey}`;

    let stock: Stock = {
      ticker: 'placeholder',
      company: 'placeholder',
      lastPrice: 'placeholder',
    };

    return this.http.get(apiUrl).pipe(
      map((resp: any) => {
        stock.ticker = resp['Meta Data']['2. Symbol'];
        stock.company = resp['Meta Data']['4. Output Size'];

        const dates = Object.keys(resp['Time Series (Daily)']);
        const mostRecentDate = dates[0];

        stock.lastPrice =
          resp['Time Series (Daily)'][mostRecentDate]['4. close'];

        return stock;
      })
    );
  }

  public autoSearch(searchValue: string): Observable<StockInfo[]> {
    var params = {
      function: 'SYMBOL_SEARCH',
      keywords: searchValue,
      apikey: API_KEY_ALPHA_VANTAGE,
    };
    const apiUrl = `${this.baseUrl}?function=${params.function}&keywords=${params.keywords}&apikey=${params.apikey}`;

    return this.http.get<any>(apiUrl).pipe(
      map((data) => {
        let stocks: StockInfo[] = [];

        data.bestMatches.forEach((match: any) =>
        stocks.push( { ticker: match['1. symbol'], company: match['2. name'] })
        );
        
        return stocks;
      })
    );
  }

  public add(data: any) {
    const apiUrl = 'https://localhost:7018/api/Stock';

    return this.http.post(apiUrl, data);
  }
}
