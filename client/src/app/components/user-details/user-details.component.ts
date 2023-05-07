import { Component, Input } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserService } from 'src/services/user.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css'],
})
export class UserDetailsComponent {
  id: string = '';

  constructor(
    private userService: UserService,
    private activatedRoute: ActivatedRoute
  ) {}

  public user?: User = {
    id: 'placeholder',
    firstName: 'placeholder',
    lastName: 'placeholder',
    email: 'placeholder',
    password: 'placeholder',
    stocks: [],
  };

  ngOnInit() {
    this.activatedRoute.params.subscribe((data) => {
      //this.id = data['id'];
      this.id = '1';
    });

    this.userService.getSingleUser(this.id).subscribe((data: User) => {
      this.user = data;
      console.log(this.user);
    });
  }
}
