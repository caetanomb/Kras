import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { FlightsComponent } from './flights/flights.component';
import { ReportComponent } from './report/report.component';

const routes: Routes = [{
  path: '',
  pathMatch: 'full',
  component: FlightsComponent
}, {
  path: 'report',
  component: ReportComponent
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
