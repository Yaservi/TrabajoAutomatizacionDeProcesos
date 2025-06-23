import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AlumnoListPageComponent } from './pages/alumno-list-page.component';

const routes: Routes = [
  { path: '', redirectTo: 'alumnos', pathMatch: 'full' },
  { path: 'alumnos', component: AlumnoListPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
