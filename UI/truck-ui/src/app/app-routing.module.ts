import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'trucks', pathMatch: 'full' },
  { path: 'trucks', loadChildren: () => import('./trucks/trucks.module').then(m => m.TrucksModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
