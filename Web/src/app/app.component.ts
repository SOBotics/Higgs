import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { environment } from '../environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public isLoggedIn: boolean;
  private userName: string;

  private loginUrl: string;
  constructor(private authService: AuthService) {

  }

  ngOnInit() {
    this.loginUrl = `${environment.apiHost}/Authentication/Login?redirect_uri=${environment.webHost}%2Foauth&scope=all`;
    this.authService.GetAuthDetails().subscribe(details => {
      this.isLoggedIn = details.IsAuthenticated;
      if (this.isLoggedIn) {
        this.userName = details.RawToken.unique_name;
      }
    });
  }

  private onLogoutClicked() {
    this.authService.Logout();
  }
}
