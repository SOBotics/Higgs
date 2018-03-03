import { Routes } from '@angular/router';
import { OAuthComponent } from './oauth/oauth.component';
import { HomeComponent } from './home/home.component';
import { ReportComponent } from './report/report.component';
import { BotComponent } from './admin/bot/bot.component';

export const appRouts: Routes = [
    { path: '', component: HomeComponent },
    { path: 'report/:id', component: ReportComponent },
    { path: 'admin/bot/:id', component: BotComponent },
    { path: 'admin/bot', component: BotComponent },
    { path: 'oauth', component: OAuthComponent }
];

