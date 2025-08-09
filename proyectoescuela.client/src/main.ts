import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { Routes, provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { HomeComponent } from './app/component/home/home.component';
import { AlumnoListPageComponent } from './app/pages/alumnos/alumno-list-page.component';
import { MaestroListPageComponent } from './app/pages/maestros/maestros-list-page.component';
import { MateriaListPageComponent } from './app/pages/materias/materias-list-page.component';
import { AsistenciaListPageComponent } from './app/pages/asistencia/asistencia-list-page.component';

import { CalificacionPage } from './app/pages/calificacion/calificacion.page';


export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '', component: HomeComponent },
  { path: 'alumnos', component: AlumnoListPageComponent },
  { path: 'maestros', component: MaestroListPageComponent },
  { path: 'materias', component: MateriaListPageComponent },
  { path: 'asistencias', component: AsistenciaListPageComponent },
  { path: 'calificaciones', component: CalificacionPage }


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
