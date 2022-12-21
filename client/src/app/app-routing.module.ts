import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BasketModule } from './basket/basket.module';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';

const routes: Routes = [
  {path: '', component: HomeComponent, data: {breadCrums: 'Home'}},
  {path: 'server-error', component: ServerErrorComponent, data: {breadCrums: 'Server error'}},
  {path: 'not-found', component: NotFoundComponent, data: {breadCrums: 'Not found'}},
  {path: 'basket', loadChildren:() =>  import('./basket/basket.module').then((module) => module.BasketModule)},
  {path: 'account', loadChildren: () => import('./account/account.module').then((module) => module.AccountModule)},
  {path: '**', redirectTo: 'not-found', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
