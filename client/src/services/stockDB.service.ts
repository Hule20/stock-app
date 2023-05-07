import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { StockDto } from "src/app/components/stock-list/stock-list.component";
import { StockInfo } from "src/app/models/stockInfo";

@Injectable({
    providedIn: 'root',
  })
  export class StockDBService {

    constructor(private http: HttpClient){}
    
    public getAll(){
        const apiUrl = 'https://localhost:7018/api/Stock';
        
        return this.http.get<StockDto[]>(apiUrl);
    }

    public add(data: StockInfo, id: string) {
      const apiUrl = `https://localhost:7018/api/UserStock/${id}`;
  
      return this.http.patch(apiUrl, data);
    }
  }