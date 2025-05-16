import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CdbService, CdbRequest, CdbResponse } from '../../services/cdb.service';

@Component({
  selector: 'app-cdb-calculator',
  templateUrl: './cdb-calculator.component.html',
  styleUrls: ['./cdb-calculator.component.scss'],
  standalone: false
})
export class CdbCalculatorComponent {
  form: FormGroup;
  result: CdbResponse | null = null;
  loading = false;
  error = '';

  constructor(
    private fb: FormBuilder,
    private cdbService: CdbService
  ) {
    this.form = this.fb.group({
      initialValue: ['', [Validators.required, Validators.min(0.01)]],
      months: ['', [Validators.required, Validators.min(2), Validators.max(360)]]
    });
  }

  calculate(): void {
    if (this.form.invalid) {
      this.markFormTouched();
      return;
    }

    this.loading = true;
    this.error = '';
    this.result = null;

    const request: CdbRequest = {
      initialValue: this.form.value.initialValue,
      months: this.form.value.months
    };

    this.cdbService.calculateCdb(request).subscribe({
      next: (response) => {
        console.log(response)
        this.result = response;
        this.loading = false;
      },
      error: (err) => {
        this.error = err.error || 'Erro ao calcular CDB';
        this.loading = false;
      }
    });
  }

  reset(): void {
    this.form.reset();
    this.result = null;
    this.error = '';
  }

  private markFormTouched(): void {
    Object.values(this.form.controls).forEach(control => {
      control.markAsTouched();
    });
  }
}
