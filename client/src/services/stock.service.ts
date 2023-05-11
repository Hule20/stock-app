import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Stock } from 'src/app/models/stock';
import { StockInfo } from 'src/app/models/stockInfo';
import { map } from 'rxjs/operators';
import { API_KEY_ALPHA_VANTAGE } from 'src/config';
import { Observable } from 'rxjs';

export interface CompanyInfo {
  name: string,
  symbol: string,
  description: string,
  exchange: string,
  sector: string,
  industry: string,
  highestYearly: string,
  lowestYearly: string,
  marketCap: string,
  sharesOutstanding: string,
  dividendDate: string,
  exDividendDate: string,
};

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
          stocks.push({ ticker: match['1. symbol'], company: match['2. name'] })
        );

        return stocks;
      })
    );
  }

  public getCompanyInfo(searchValue: string) {
    var params = {
      function: 'OVERVIEW',
      symbol: searchValue,
      apikey: API_KEY_ALPHA_VANTAGE,
    };
    const apiUrl = `${this.baseUrl}?function=${params.function}&symbol=${params.symbol}&apikey=${params.apikey}`;

    console.log(apiUrl)

    return this.http.get(apiUrl).pipe(
      map((response: any) => {
        let stockInfo: CompanyInfo = {
          name: response['Name'],
          symbol: response['Symbol'],
          description: response['Description'],
          exchange: response['Exchange'],
          sector: response['Sector'],
          industry: response['Industry'],
          highestYearly: response['HighestYearly'],
          lowestYearly: response['LowestYearly'],
          marketCap: response['MarketCap'],
          sharesOutstanding: response['SharesOutstanding'],
          dividendDate: response['DividendDate'],
          exDividendDate: response['ExDividendDate'],
        };
    
        return stockInfo;
      })
    );
  }
}
