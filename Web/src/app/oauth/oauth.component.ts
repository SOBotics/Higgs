import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Configuration } from '../../swagger-gen';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-oauth',
  templateUrl: './oauth.component.html',
  styleUrls: ['./oauth.component.scss']
})
export class OAuthComponent implements OnInit {

  constructor(private authConfiguration: Configuration,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService) {

  }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      const accessToken = params['access_token'];
      if (accessToken) {
        this.authService.Login(accessToken);
        this.router.navigateByUrl('/');
      }
    });
  }
}
