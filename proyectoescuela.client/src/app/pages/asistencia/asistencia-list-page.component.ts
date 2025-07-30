import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AsistenciaRegisterComponent } from '../../component/asistencia/register_asistencia/asistencia-register.component';
import { AsistenciaListComponent } from '../../component/asistencia/list_asistencia/asistencia-list.component';
import { AsistenciaService } from '../../services/asistencia/asistencia-service';
import { AlumnoService } from '../../services/alumnos/alumno-service';
import { MateriaService } from '../../services/materias/materia-service';

@Component({
  selector: 'app-asistencia-list-page',
  standalone: true,
  imports: [CommonModule, AsistenciaRegisterComponent, AsistenciaListComponent],
  templateUrl: './asistencia-list-page.component.html',
  styleUrls: ['./asistencia-list-page.component.css']
})
export class AsistenciaListPageComponent {
  asistencias: any[] = [];
  alumnos: any[] = [];
  materias: any[] = [];
  mostrarModal = false;
  asistenciaAEditar: any = null;

  constructor(
    private asistenciaService: AsistenciaService,
    private alumnoService: AlumnoService,
    private materiaService: MateriaService
  ) {
    this.cargarAsistencias();
    this.cargarAlumnos();
    this.cargarMaterias();
  }

  cargarAsistencias() {
    this.asistenciaService.obtenerAsistencias().subscribe({
      next: data => {
        console.log('Asistencias recibidas:', data.items);
        this.asistencias = data.items ?? [];
      },
      error: () => this.asistencias = []
    });
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

  cargarMaterias() {
    this.materiaService.obtenerMaterias().subscribe({
      next: data => {
        console.log('Materias recibidas:', data.items);
        this.materias = data.items ?? [];
      },
      error: () => this.materias = []
    });
  }

  editarAsistencia(asistencia: any) {
    this.asistenciaAEditar = asistencia;
    this.mostrarModal = true;
  }

  cerrarModal() {
    this.mostrarModal = false;
    this.asistenciaAEditar = null;
    this.cargarAsistencias();
  }

  borrarAsistencia(asistencia: any) {
    if (confirm(`¿Seguro que deseas borrar la asistencia del ${asistencia.fechaAsistencia}?`)) {
      this.asistenciaService.borrarAsistencia(asistencia.id).subscribe({
        next: () => this.cargarAsistencias(),
        error: () => alert('Error al borrar la asistencia.')
      });
    }
  }
}
