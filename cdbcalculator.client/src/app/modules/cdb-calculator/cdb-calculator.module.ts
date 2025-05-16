import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { CdbCalculatorComponent } from './components/cdb-calculator/cdb-calculator.component';

const routes: Routes = [
  {
    path: '',
    component: CdbCalculatorComponent
  }
];

@NgModule({
  declarations: [
    CdbCalculatorComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ]
})
export class CdbCalculatorModule { }
