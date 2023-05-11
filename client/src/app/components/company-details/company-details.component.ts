import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StockService } from 'src/services/stock.service';

@Component({
  selector: 'app-company-details',
  templateUrl: './company-details.component.html',
  styleUrls: ['./company-details.component.css']
})
export class CompanyDetailsComponent {
    symbol: string = '';

    constructor(private stockService: StockService, private activatedRoute: ActivatedRoute){}

    companyDetails = {
      symbol: '',
      name: '',
      description: '',
      exchange: '',
      sector: '',
      industry: '',
      highestYearly: '',
      lowestYearly: '',
      marketCap: '',
      sharesOutstanding: '',
      dividendDate: '',
      exDividendDate: '',
    };
    
    ngOnInit() {
      this.activatedRoute.params.subscribe((params) => {
        this.symbol = params['symbol'];
      });
  
      this.stockService.getCompanyInfo(this.symbol).subscribe((data: any) => {
        this.companyDetails = data;
        console.log(this.companyDetails);  
      });
    }
}
