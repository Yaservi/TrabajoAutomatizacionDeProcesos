import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AlumnoListPageComponent } from './pages/alumnos/alumno-list-page.component';
import { MaestroListPageComponent } from './pages/maestros/maestros-list-page.component';
import { MateriaListPageComponent } from './pages/materias/materias-list-page.component';
import { AsistenciaListPageComponent } from './pages/asistencia/asistencia-list-page.component';
import { HomeComponent } from './component/home.component';

// calificaciones
import { CalificacionPage } from './pages/calificacion/calificacion.page';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'alumnos', component: AlumnoListPageComponent },
  { path: 'maestros', component: MaestroListPageComponent },
  { path: 'materias', component: MateriaListPageComponent },
  { path: 'asistencias', component: AsistenciaListPageComponent },
  { path: 'calificaciones', component: CalificacionPage }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
