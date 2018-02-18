import { Component, OnInit, Inject } from '@angular/core';
import { BASE_PATH, AdminApi } from '../../swagger-gen';
import { environment } from '../../environments/environment';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  private isLoggedIn: boolean;
  private userName: string;

  private loginUrl: string;
  constructor(private authService: AuthService) {

  }

  ngOnInit() {
    this.loginUrl = `${environment.apiHost}/Authentication/Login?redirect_uri=${environment.webHost}%2Foauth&scope=all`;
    this.isLoggedIn = this.authService.IsAuthenticated();
    if (this.isLoggedIn) {
      this.userName = this.authService.GetTokenData().unique_name;
    }
    this.authService.GetScopes();
  }

  private onLogoutClicked() {
    this.authService.Logout();
  }
}
