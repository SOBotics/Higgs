import { Routes } from '@angular/router';
import { OAuthComponent } from './oauth/oauth.component';
import { HomeComponent } from './home/home.component';

export const appRouts: Routes = [
    { path: '', component: HomeComponent },
    { path: 'oauth', component: OAuthComponent }
];

