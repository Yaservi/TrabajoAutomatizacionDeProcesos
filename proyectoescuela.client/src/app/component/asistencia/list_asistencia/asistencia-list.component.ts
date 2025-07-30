import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-asistencia-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './asistencia-list.component.html',
  styleUrls: ['./asistencia-list.component.css']
})
export class AsistenciaListComponent {
  @Input() asistencias: any[] = [];
  @Output() editarAsistencia = new EventEmitter<any>();
  @Output() borrarAsistencia = new EventEmitter<any>();

  editarAsistenciaHandler(asistencia: any) {
    this.editarAsistencia.emit(asistencia);
  }

  borrarAsistenciaHandler(asistencia: any) {
    this.borrarAsistencia.emit(asistencia);
  }
}
