import { Component } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserService } from 'src/services/user.service';
import { MatDialog } from '@angular/material/dialog';
import { AddUserDialogComponent } from '../add-user-dialog/add-user-dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css'],
})
export class UserListComponent {
  constructor(private userService: UserService, private dialogRef: MatDialog, private router: Router) {}

  public dataSource: User[] = [];
  displayedColumns: string[] = ['ID', 'first-name', 'last-name', 'email', 'password', 'action'];

  ngOnInit() {
    this.userService.getUsers().subscribe((resp: User[]) => {
      this.dataSource = resp;
    });
  }

  openDialog(){
    this.dialogRef.open(AddUserDialogComponent);
  }

  deleteUser(id: string){
    this.userService.delete(id).subscribe({
      next: ((res: any) => {
        this.router.navigate(['Dashboard']);
      }),
      error: ((err: any) => {
        console.log(err);
      })
    })
  }
}
