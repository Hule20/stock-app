import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-add-user-dialog',
  templateUrl: './add-user-dialog.component.html',
  styleUrls: ['./add-user-dialog.component.css']
})
export class AddUserDialogComponent {

  userForm!: FormGroup;

  constructor(private fb: FormBuilder, private userService: UserService, private router: Router){}

  ngOnInit(){
    this.userForm = this.fb.group({
      firstName: '',
      lastName: '',
      email: '',
      password: '',
    })
  }

  createUser(){
    this.userService.create(this.userForm.value).subscribe({
      next: (() => {
        this.router.navigate(['Dashboard']);
      })
    });
  }
}
