

import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListCalificacionComponent } from '../../component/calificacion/list_calificacion/list_calificacion.component';
import { RegisterCalificacionComponent } from '../../component/calificacion/register_calificacion/register_calificacion.component';
import { UpdateCalificacionComponent } from '../../component/calificacion/update_calificacion/update_calificacion.component';
import { CalificacionService, Calificacion } from '../../services/calificacion/calificacion.service';

@Component({
  selector: 'app-calificacion-page',
  standalone: true,
  imports: [
    CommonModule,
    ListCalificacionComponent,
    RegisterCalificacionComponent,
    UpdateCalificacionComponent
  ],
  templateUrl: './calificacion.page.html',
  styleUrls: ['./calificacion.page.scss']
})
export class CalificacionPage {
  calificacionSeleccionada: Calificacion | null = null;

  mostrarLista = true;
  mostrarRegistro = false;
  mostrarEdicion = false;
  mostrarModal = false;
  modoEdicion = false;

  @ViewChild(ListCalificacionComponent) listComponent!: ListCalificacionComponent;

  constructor(private calificacionService: CalificacionService) { }

  abrirRegistro(): void {
    this.modoEdicion = false;
    this.calificacionSeleccionada = null;
    this.mostrarModal = true;
  }

  abrirEdicion(calificacion: Calificacion): void {
    this.modoEdicion = true;
    this.calificacionSeleccionada = calificacion;
    this.mostrarModal = true;
  }

  cerrarModal(): void {
    this.mostrarModal = false;
    this.modoEdicion = false;
    this.calificacionSeleccionada = null;
  }

  onCalificacionActualizada(calificacionActualizada: Calificacion) {
    this.cerrarModal();
    if (this.listComponent) {
      this.listComponent.actualizarCalificacionEnLista(calificacionActualizada);
    }
  }

  onCalificacionEliminada(idEliminado: string): void {
    if (this.listComponent) {
      this.listComponent.obtenerCalificaciones(); // actualiza la lista
    }
  }

  onCalificacionRegistrada(): void {
    if (this.listComponent) {
      this.listComponent.obtenerCalificaciones();
    }
    this.cerrarModal();
  }


}
