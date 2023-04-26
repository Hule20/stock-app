import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { StockService } from 'src/services/stock.service';
import { AppComponent } from './app.component';
import { StockCardComponent } from './components/stock-card/stock-card.component';
import { HttpClientModule } from '@angular/common/http';
import { UserListComponent } from './components/user-list/user-list.component';
import { UserService } from 'src/services/user.service';
import { Routes, RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { UserDetailsComponent } from './components/user-details/user-details.component';

const appRoute : Routes = [
  { path: 'Stocks', component: StockCardComponent},
  { path: 'Users', component: UserListComponent },
  { path: 'Users/:id', component: UserDetailsComponent },
  { path: '**', component: PageNotFoundComponent },
]

@NgModule({
  declarations: [
    AppComponent,
    StockCardComponent,
    UserListComponent,
    PageNotFoundComponent,
    UserDetailsComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(appRoute)
  ],
  providers: [StockService],
  bootstrap: [AppComponent]
})
export class AppModule { }
