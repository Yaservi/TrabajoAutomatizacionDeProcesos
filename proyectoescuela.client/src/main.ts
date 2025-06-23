import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { Routes, provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http'; 
import { AlumnoListPageComponent } from './app/pages/alumno-list-page.component';

export const routes: Routes = [
  { path: '', redirectTo: 'alumnos', pathMatch: 'full' },
  { path: 'alumnos', component: AlumnoListPageComponent }
];

export const appRouterProviders = [
  provideRouter(routes)
];

bootstrapApplication(AppComponent, {
  providers: [
    appRouterProviders,
    provideHttpClient()
  ]
}).catch(err => console.error(err));
