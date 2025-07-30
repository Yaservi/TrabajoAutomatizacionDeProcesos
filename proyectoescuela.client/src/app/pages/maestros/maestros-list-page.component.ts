import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaestroRegisterComponent } from '../../component/maestros/register_maestros/maestro-register.component';
import { MaestroListComponent } from '../../component/maestros/list_maestros/maestro-list.component';
import { MaestroService } from '../../services/maestros/maestro-service';

@Component({
  selector: 'app-maestro-list-page',
  standalone: true,
  imports: [CommonModule, MaestroRegisterComponent, MaestroListComponent],
  templateUrl: './maestros-list-page.component.html',
  styleUrls: ['./maestros-list-page.component.css']
})
export class MaestroListPageComponent {
  maestros: any[] = [];
  mostrarModal = false;
  maestroAEditar: any = null;

  constructor(private maestroService: MaestroService) {
    this.cargarMaestros();
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

  editarMaestro(maestro: any) {
    this.maestroAEditar = maestro;
    this.mostrarModal = true;
  }

  cerrarModal() {
    this.mostrarModal = false;
    this.maestroAEditar = null;
    this.cargarMaestros();
  }

  borrarMaestro(maestro: any) {
    if (confirm(`¿Seguro que deseas borrar a ${maestro.nombre} ${maestro.apellido}?`)) {
      this.maestroService.borrarMaestro(maestro.id).subscribe({
        next: () => this.cargarMaestros(),
        error: () => alert('Error al borrar el maestro.')
      });
    }
  }
}
