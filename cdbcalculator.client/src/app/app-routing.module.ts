import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'cdb-calculator', pathMatch: 'full' },
  { path: 'cdb-calculator', loadChildren: () => import('./modules/cdb-calculator/cdb-calculator.module').then(m => m.CdbCalculatorModule) },
  { path: '**', redirectTo: 'cdb-calculator' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
