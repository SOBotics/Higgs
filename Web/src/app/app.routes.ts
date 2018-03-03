import { Routes, UrlSegment } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ReportComponent } from './report/report.component';
import { BotComponent } from './admin/bot/bot.component';
import { BotsComponent } from './admin/bots/bots.component';
import { AdminHomeComponent } from './admin/admin-home/admin-home.component';
import { ReportsComponent } from './reports/reports.component';

export const reservedPaths = /admin|reports/;

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },


    // { path: 'report/:id', component: ReportComponent },

    { path: 'admin', component: AdminHomeComponent },
    { path: 'admin/bots', component: BotsComponent },
    { path: 'admin/bot', component: BotComponent },
    { path: 'admin/bot/:id', component: BotComponent },

    { path: 'reports', component: ReportsComponent },
    { path: 'report/:id', component: ReportComponent },

    // Handle things like
    // /higgs
    // /higgs/report
    {
        matcher: makeMatcher('report', url => ({ id: url[2] })), component: ReportComponent
    }
];

function makeMatcher(path: string, paramGetter: (url: UrlSegment[]) => any) {
    return (url: UrlSegment[]) => {
        if (url.length < 2) {
            return null;
        }
        // If it's a reserved path, we can't handle it.
        if (url[0].path.match(reservedPaths)) {
            return null;
        }
        if (url[1].path === path) {
            return { consumed: url, posParams: paramGetter(url) };
        }
    };
}

