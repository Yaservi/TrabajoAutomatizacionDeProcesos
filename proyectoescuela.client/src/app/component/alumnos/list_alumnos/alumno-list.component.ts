import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-alumno-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './alumno-list.component.html',
  styleUrls: ['./alumno-list.component.css']
})
export class AlumnoListComponent {
  @Input() alumnos: any[] = [];
  @Output() editarAlumno = new EventEmitter<any>();
  @Output() borrarAlumno = new EventEmitter<any>();


  editarAlumnoHandler(alumno: any) {
    this.editarAlumno.emit(alumno);
  }


  borrarAlumnoHandler(alumno: any) {
    this.borrarAlumno.emit(alumno);
  }
}
