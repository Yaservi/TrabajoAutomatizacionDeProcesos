import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './component/navbar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  template: `
    <app-navbar></app-navbar>
    <router-outlet></router-outlet>
  `,
  imports: [NavbarComponent, RouterOutlet]
})
export class AppComponent { }

