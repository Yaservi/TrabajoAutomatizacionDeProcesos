import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MateriaRegisterComponent } from '../../component/materias/register_materias/materias-register.component';
import { MateriaListComponent } from '../../component/materias/list_materias/materias-list.component';
import { MateriaService } from '../../services/materias/materia-service';
import { MaestroService } from '../../services/maestros/maestro-service';

@Component({
  selector: 'app-materia-list-page',
  standalone: true,
  imports: [CommonModule, MateriaRegisterComponent, MateriaListComponent],
  templateUrl: './materias-list-page.component.html',
  styleUrls: ['./materias-list-page.component.css']
})
export class MateriaListPageComponent {
  materias: any[] = [];
  maestros: any[] = [];
  mostrarModal = false;
  materiaAEditar: any = null;

  constructor(
    private materiaService: MateriaService,
    private maestroService: MaestroService
  ) {
    this.cargarMaterias();
    this.cargarMaestros();
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

  cargarMaestros() {
    this.maestroService.getMaestros().subscribe({
      next: data => {
        console.log('Maestros recibidos:', data.items);
        this.maestros = data.items ?? [];
      },
      error: () => this.maestros = []
    });
  }

  editarMateria(materia: any) {
    this.materiaAEditar = materia;
    this.mostrarModal = true;
  }

  cerrarModal() {
    this.mostrarModal = false;
    this.materiaAEditar = null;
    this.cargarMaterias();
  }

  borrarMateria(materia: any) {
    if (confirm(`¿Seguro que deseas borrar la materia "${materia.nombreMateria}"?`)) {
      this.materiaService.borrarMateria(materia.id).subscribe({
        next: () => this.cargarMaterias(),
        error: () => alert('Error al borrar la materia.')
      });
    }
  }
}
