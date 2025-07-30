import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlumnoRegisterComponent } from '../../component/alumnos/register_alumnos/alumno-register.component';
import { AlumnoListComponent } from '../../component/alumnos/list_alumnos/alumno-list.component';
import { AlumnoService } from '../../services/alumnos/alumno-service';

@Component({
  selector: 'app-alumno-list-page',
  standalone: true,
  imports: [CommonModule, AlumnoRegisterComponent, AlumnoListComponent],
  templateUrl: './alumno-list-page.component.html', 
  styleUrls: ['./alumno-list-page.component.css']
})
export class AlumnoListPageComponent { 
  alumnos: any[] = [];
  mostrarModal = false;

  constructor(private alumnoService: AlumnoService) {
    this.cargarAlumnos();
  }

  cargarAlumnos() {
    this.alumnoService.getAlumnos().subscribe({
      next: data => {
        console.log('Alumnos recibidos:', data.items);
        this.alumnos = data.items ?? [];
      },
      error: () => this.alumnos = []
    });
  }

  alumnoAEditar: any = null;

  editarAlumno(alumno: any) {
    this.alumnoAEditar = alumno;
    this.mostrarModal = true;
  }

  cerrarModal() {
    this.mostrarModal = false;
    this.alumnoAEditar = null;
    this.cargarAlumnos();
  }

  borrarAlumno(alumno: any) {
    if (confirm(`¿Seguro que deseas borrar a ${alumno.nombre}?`)) {
      this.alumnoService.borrarAlumno(alumno.id).subscribe({
        next: () => this.cargarAlumnos(),
        error: () => alert('Error al borrar el alumno.')
      });
    }
  }
}
