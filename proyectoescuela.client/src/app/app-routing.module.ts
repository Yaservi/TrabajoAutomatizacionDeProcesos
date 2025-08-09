import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './component/home/home.component';
import { AlumnoListPageComponent } from './pages/alumnos/alumno-list-page.component';
import { MaestroListPageComponent } from './pages/maestros/maestros-list-page.component';
import { MateriaListPageComponent } from './pages/materias/materias-list-page.component';
import { AsistenciaListPageComponent } from './pages/asistencia/asistencia-list-page.component';


import { CalificacionPage } from './pages/calificacion/calificacion.page';

const routes: Routes = [
  { path: '', component: HomeComponent }, 
  { path: 'home', component: HomeComponent }, 
  { path: 'alumnos', component: AlumnoListPageComponent },
  { path: 'maestros', component: MaestroListPageComponent },
  { path: 'materias', component: MateriaListPageComponent },
  { path: 'asistencias', component: AsistenciaListPageComponent },
  { path: 'calificaciones', component: CalificacionPage },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
