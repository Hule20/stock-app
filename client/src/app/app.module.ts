import { Component, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { StockService } from 'src/services/stock.service';
import { AppComponent } from './app.component';
import { StockCardComponent } from './components/stock-card/stock-card.component';
import { HttpClientModule } from '@angular/common/http';
import { UserListComponent } from './components/user-list/user-list.component';
import { Routes, RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { UserDetailsComponent } from './components/user-details/user-details.component';
import { StockSearchComponent } from './components/stock-search/stock-search.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BannerComponent } from './components/logo/banner/banner.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AddUserDialogComponent } from './components/add-user-dialog/add-user-dialog.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';

const appRoute: Routes = [
  { path: '', component: StockSearchComponent },
  { path: 'Stocks', component: StockCardComponent },
  {
    path: 'Dashboard',
    component: DashboardComponent,
    children: [
      {
        path: 'Users',
        component: UserListComponent,
      },
    ],
  },
  { path: 'Users/:id', component: UserDetailsComponent },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    StockCardComponent,
    UserListComponent,
    PageNotFoundComponent,
    UserDetailsComponent,
    StockSearchComponent,
    BannerComponent,
    DashboardComponent,
    AddUserDialogComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(appRoute),
    CommonModule,
    FormsModule,
    NoopAnimationsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatButtonModule,
  ],
  providers: [StockService],
  bootstrap: [AppComponent],
})
export class AppModule {}
