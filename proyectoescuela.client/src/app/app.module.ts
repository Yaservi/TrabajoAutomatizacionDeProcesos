import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AlumnoRegisterComponent } from './component/alumnos/register_alumnos/alumno-register.component';
import { MaestroListComponent } from './component/maestros/list_maestros/maestro-list.component';
import { MaestroListPageComponent } from './pages/maestros/maestros-list-page.component';
import { MateriaListPageComponent } from './pages/materias/materias-list-page.component';
import { AsistenciaListPageComponent } from './pages/asistencia/asistencia-list-page.component';
import { AsistenciaListComponent } from './component/asistencia/list_asistencia/asistencia-list.component';
import { AsistenciaRegisterComponent } from './component/asistencia/register_asistencia/asistencia-register.component';
import { MateriaListComponent } from './component/materias/list_materias/materias-list.component';
import { MateriaRegisterComponent } from './component/materias/register_materias/materias-register.component';
import { HomeComponent } from './component/home.component';


// para calificacion
import { RegisterCalificacionComponent } from './component/calificacion/register_calificacion/register_calificacion.component';
import { UpdateCalificacionComponent } from './component/calificacion/update_calificacion/update_calificacion.component';
import { ListCalificacionComponent } from './component/calificacion/list_calificacion/list_calificacion.component';
import { CalificacionPage } from './pages/calificacion/calificacion.page';


@NgModule({
  declarations: [
    AppComponent,
    MaestroListComponent,
    MaestroListPageComponent,
    AsistenciaListComponent,
    AsistenciaRegisterComponent,
    MateriaListComponent,
    MateriaRegisterComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    AlumnoRegisterComponent, // <-- aquí,
    MateriaListPageComponent,
    AsistenciaListPageComponent,
    HomeComponent,


    //  Calificación
    RegisterCalificacionComponent,
    UpdateCalificacionComponent,
    ListCalificacionComponent,
    CalificacionPage

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
