import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // Para ngModel

import { CalificacionService, Calificacion } from '../../../services/calificacion/calificacion.service';
import { AlumnoService, Alumno } from '../../../services/alumnos/alumno-service';
import { MateriaService, Materia } from '../../../services/materias/materia-service';

@Component({
  standalone: true,
  selector: 'app-list-calificacion',
  templateUrl: './list_calificacion.component.html',
  styleUrls: ['./list_calificacion.component.scss'],
  imports: [CommonModule, FormsModule] // Asegúrate de incluir FormsModule
})
export class ListCalificacionComponent implements OnInit {
  calificaciones: Calificacion[] = [];
  alumnos: Alumno[] = [];
  materias: Materia[] = [];
  cargando: boolean = false;

  filtroAlumno: string = ''; // <-- Campo de búsqueda

  @Output() editar = new EventEmitter<Calificacion>();
  @Output() eliminado = new EventEmitter<string>();

  constructor(
    private calificacionService: CalificacionService,
    private alumnoService: AlumnoService,
    private materiaService: MateriaService
  ) { }

  ngOnInit(): void {
    this.cargarAlumnos();
    this.cargarMaterias();
    this.obtenerCalificaciones();
  }

  cargarAlumnos(): void {
    this.alumnoService.getTodosAlumnos().subscribe({
      next: (alumnos) => this.alumnos = alumnos,
      error: (err) => {
        console.error('Error al cargar alumnos', err);
        this.alumnos = [];
      }
    });
  }

  cargarMaterias(): void {
    this.materiaService.obtenerTodasMaterias().subscribe({
      next: (materias) => this.materias = materias,
      error: (err) => {
        console.error('Error al cargar materias', err);
        this.materias = [];
      }
    });
  }

  obtenerCalificaciones(): void {
    this.cargando = true;
    this.calificacionService.getCalificaciones().subscribe({
      next: (respuesta) => {
        this.calificaciones = respuesta.items || [];
        this.cargando = false;
      },
      error: (error) => {
        console.error('Error al obtener calificaciones', error);
        this.cargando = false;
      }
    });
  }

  calificacionesFiltradas(): Calificacion[] {
    const filtro = this.filtroAlumno.toLowerCase().trim();
    return this.calificaciones.filter(cal => {
      const nombreAlumno = this.obtenerNombreAlumno(cal.idAlumno).toLowerCase();
      return nombreAlumno.includes(filtro);
    });
  }

  obtenerNombreAlumno(idAlumno: string): string {
    const alumno = this.alumnos.find(a => a.id === idAlumno);
    return alumno ? `${alumno.nombre} ${alumno.apellido}` : 'Alumno no encontrado';
  }

  obtenerNombreMateria(idMateria: string): string {
    const materia = this.materias.find(m => m.id === idMateria);
    return materia ? materia.nombreMateria : 'Materia no encontrada';
  }

  editarCalificacion(calificacion: Calificacion): void {
    this.editar.emit(calificacion);
  }

  eliminarCalificacion(id: string): void {
    if (confirm('¿Seguro que deseas eliminar esta calificación?')) {
      this.calificacionService.borrarCalificacion(id).subscribe({
        next: () => {
          alert('Calificación eliminada');
          this.obtenerCalificaciones();
          this.eliminado.emit(id);
        },
        error: err => {
          console.error('Error al eliminar', err);
          alert('Error al eliminar calificación');
        }
      });
    }
  }

  actualizarCalificacionEnLista(calificacionActualizada: Calificacion) {
    const index = this.calificaciones.findIndex(c => c.id === calificacionActualizada.id);
    if (index !== -1) {
      this.calificaciones[index] = calificacionActualizada;
    }
  }

  obtenerLeyendaNota(nota: number | string): string {
    const n = Number(nota);
    if (isNaN(n)) return '';

    if (n >= 90 && n <= 100) return 'A';
    if (n >= 80 && n <= 89) return 'B';
    if (n >= 70 && n <= 79) return 'C';
    if (n >= 60 && n <= 69) return 'D';
    if (n >= 1 && n <= 59) return 'F';
    return 'FI'; // 0 o inasistencia
  }
}
