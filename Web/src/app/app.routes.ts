import { Routes } from '@angular/router';
import { OAuthComponent } from './oauth/oauth.component';
import { HomeComponent } from './home/home.component';
import { ReportComponent } from './report/report.component';
import { BotComponent } from './admin/bot/bot.component';
import { BotsComponent } from './admin/bots/bots.component';
import { AdminHomeComponent } from './admin/admin-home/admin-home.component';

export const appRouts: Routes = [
    { path: '', component: HomeComponent },
    { path: 'report/:id', component: ReportComponent },

    { path: 'admin', component: AdminHomeComponent },
    { path: 'admin/bots', component: BotsComponent },
    { path: 'admin/bot', component: BotComponent },
    { path: 'admin/bot/:id', component: BotComponent },
    { path: 'oauth', component: OAuthComponent }
];

