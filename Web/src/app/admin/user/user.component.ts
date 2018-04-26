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
  private userId: number;


  constructor(private adminService: AdminService, private route: ActivatedRoute) { }

  ngOnInit() {
    const allScopes = this.adminService.adminScopesGet();
    this.route.params.subscribe(params => {
      this.userId = +params['id'];
      if (this.userId) {
        allScopes.combineLatest(this.adminService.adminUserGet(this.userId), (scopes, user) => ({
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

  public async updateUser() {
    const assignedScopes = this.user.Scopes.filter(scope => scope.Assigned)
      .map(scope => scope.Name);

    this.adminService.adminUserPost({
      id: this.userId,
      scopes: assignedScopes
    })
      .subscribe(a => window.location.reload());
  }
}
