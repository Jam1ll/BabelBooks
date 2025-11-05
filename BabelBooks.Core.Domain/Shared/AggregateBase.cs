namespace BabelBooks.Core.Domain.Shared
{
    public abstract class AggregateBase
    {
        public Guid Id { get; protected set; }

        private readonly List<object> _uncommitedEvents = []; //eventos que han ocurrido pero no han sido guardados

        public object[] GetUncommitedEvents() => [.. _uncommitedEvents]; //metodo para obtener los eventos no guardados

        public void ClearUncommitedEvents()=> _uncommitedEvents.Clear(); //metodo para limpiar la lista de eventos luego de guardar

        protected void ApplyEvent(object @event) //metodo para aplicar un evento al estado del agregado actual
        {
            var method = GetType().GetMethod("ApplyEvent", [@event.GetType()]) //llamar al metodo ApplyEvent especifico para el tipo de evento mediante reflection
                ?? throw new InvalidOperationException($"No se encontro el metodo ApplyEvent para el evento de tipo {@event.GetType().Name}"); //si no se encuentra el metodo...
            method.Invoke(this, [@event]);
        }
        
        public void RaiseEvent(object @event) //metodo para que las clases derivadas registren un nuevo evento
        {
            ApplyEvent(@event); //aplicar evento al estado actual del agregado
            _uncommitedEvents.Add(@event); //agregar evento a la lista de eventos no guardados
        }
    }
}
