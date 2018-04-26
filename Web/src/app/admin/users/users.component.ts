import { Component, OnInit } from '@angular/core';
import { AdminService, UsersResponse } from '../../../swagger-gen';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  public users: UsersResponse[];
  constructor(private adminService: AdminService) { }

  ngOnInit() {
    this.adminService.adminUsersGet().subscribe(r => {
      this.users = r;
    });
  }
}
