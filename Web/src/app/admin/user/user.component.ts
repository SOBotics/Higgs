import { Component, OnInit } from '@angular/core';
import { UsersResponse, AdminService } from '../../../swagger-gen';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  public scopes: string[];
  public user: {
    Id: number;
    Name: string;
    Scopes: { Name: string, Assigned: boolean }[]
  };


  constructor(private adminService: AdminService, private route: ActivatedRoute) { }

  ngOnInit() {
    const allScopes = this.adminService.adminScopesGet();
    this.route.params.subscribe(params => {
      const userId = +params['id'];
      if (userId) {
        allScopes.combineLatest(this.adminService.adminUserGet(userId), (scopes, user) => ({
          Scopes: scopes,
          User: user
        }))
          .subscribe(r => {
            this.scopes = r.Scopes;

            const userScopes = [];
            for (let i = 0; i < r.Scopes.length; i++) {
              const scopeName = r.Scopes[i];
              userScopes.push({ Name: scopeName, Assigned: r.User.scopes.indexOf(scopeName) !== -1 });
            }

            this.user = {
              Id: r.User.userId,
              Name: r.User.displayName,
              Scopes: userScopes
            };
          });
      }
    });
  }
}
