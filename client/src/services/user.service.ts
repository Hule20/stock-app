import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs';
import { User } from 'src/app/models/user';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  public getUsers() {
    const apiUrl = 'https://localhost:7018/api/User';

    return this.http.get<User[]>(apiUrl);
  }

  public getSingleUser(id: string) {
    const apiUrl = `https://localhost:7018/api/User/${id}`;

    return this.http.get<User>(apiUrl);
  }

  public create(data: any) {
    const apiUrl = 'https://localhost:7018/api/User';

    return this.http.post(apiUrl, data);
  }

  public delete(id: string) {
    const apiUrl = `https://localhost:7018/api/User/${id}`;

    return this.http.delete(apiUrl);
  }

  public addStock(stockId: string) {
    const apiUrl = `https://localhost:7018/api/UserStock/${stockId}`;
  }
}
