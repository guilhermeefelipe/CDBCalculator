import { Component } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  standalone: false
})
export class AppComponent {
  title = 'Calculadora CDB';
  currentYear = new Date().getFullYear();
  isCalculatorPage = false; // Para estilização condicional

  constructor(private router: Router) {
    // Detecta mudanças de rota para estilizar o cabeçalho
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        this.isCalculatorPage = event.url === '/cdb-calculator' || event.url === '/';
      });
  }
}
