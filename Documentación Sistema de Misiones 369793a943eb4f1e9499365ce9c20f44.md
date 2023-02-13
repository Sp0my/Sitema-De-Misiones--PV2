# Documentación Sistema de Misiones

# Sistema de Misiones

## Índice

Este es un trabajo en Unity, pensado como un sistema de misiones **modular**, permitiendo añadir elementos en su UI y Scripts, dependiendo las necesidades que se deban cubrir.

Contiene elementos de TextMeshPro para sus menús, permitiendo adaptabilidad a todo tipo de Relaciones de Aspecto sin perder su organización.

## Instalación

1- Acceder al siguiente link para descargar el package de archivos de forma rápida:

[https://github.com/Sp0my/proyectoexamen_JPRR](https://github.com/Sp0my/proyectoexamen_JPRR)

2- Al descargarlo y abrirlo en tu juego de unity, los elementos se encontrarán bajo en una carpeta bajo el nombre de: **PV2-MisSys.** Dentro de ella veras las carpetas: Prefabs, Scripts, y TextMeshPro, dentro de la carpeta **Scripts** es donde encontrarás los siguientes archivos: 

![Untitled](Documentacio%CC%81n%20Sistema%20de%20Misiones%20369793a943eb4f1e9499365ce9c20f44/Untitled.png)

## Contenido

- **Base de Datos**
    
    Este es el elemento más importante de este sistema. Es desde esta base de datos que podrás crear misiones, nombrarlas, asignar su tipo, sus recompensas, recompensas especiales, puntos de experiencia y más.
    
    ![Untitled](Documentacio%CC%81n%20Sistema%20de%20Misiones%20369793a943eb4f1e9499365ce9c20f44/Untitled%201.png)
    
    Esta base de datos ya contiene 3 tipos de misiones por defecto: Matar, Recolectar y Entregar, pero pueden ser agregadas otro tipo de misiones desde el script **QuestSystem**
    
    ![Untitled](Documentacio%CC%81n%20Sistema%20de%20Misiones%20369793a943eb4f1e9499365ce9c20f44/Untitled%202.png)
    
    Solo deberás instanciar e ir tomando de base el formato de las demás misiones, agregando funciones conforme las necesites.
    
    ![Untitled](Documentacio%CC%81n%20Sistema%20de%20Misiones%20369793a943eb4f1e9499365ce9c20f44/Untitled%203.png)
    

- **Jugador**
    
    Este script será el que asignes a tu objeto de jugador, pues será el que le permitirá recibir las misiones de los NPCs y recompensas, así como editar los botones para abrir el menú.
    
    ![Untitled](Documentacio%CC%81n%20Sistema%20de%20Misiones%20369793a943eb4f1e9499365ce9c20f44/Untitled%204.png)
    
    También te servirá para unirlo al script de inventario y de experiencia que vaya a llevar tu jugador, pudiendo unirlos en uno solo y editando el apartado designado según necesites.
    
    ![Untitled](Documentacio%CC%81n%20Sistema%20de%20Misiones%20369793a943eb4f1e9499365ce9c20f44/Untitled%205.png)
    

- **ScriptsNPC**
    
    Los scripts en esta carpeta son aquellos que deberás integrar al NPC (u objecto) que quieras otorgue las misiones al jugador. 
    
    ![Untitled](Documentacio%CC%81n%20Sistema%20de%20Misiones%20369793a943eb4f1e9499365ce9c20f44/Untitled%206.png)
    
    Así mismo, cuando interactúes con un NPC se desplegará un panel para aceptar la misión y que puede ser modificado en el Hierarchy.
    
    ![Untitled](Documentacio%CC%81n%20Sistema%20de%20Misiones%20369793a943eb4f1e9499365ce9c20f44/Untitled%207.png)
    

- **QuestTracker**
    
    Este Script es de los más importantes, pues es el identificador de misiones.
    
    ![Untitled](Documentacio%CC%81n%20Sistema%20de%20Misiones%20369793a943eb4f1e9499365ce9c20f44/Untitled%208.png)
    
    Por lo que en caso de agregar algún otro tipo de misión a la base de datos, deberás actualizar este identificador con el tipo de misión que has agregado en el siguiente espacio.
    
    ![Untitled](Documentacio%CC%81n%20Sistema%20de%20Misiones%20369793a943eb4f1e9499365ce9c20f44/Untitled%209.png)
    

- **QuestTrackerPanel**
    
    ![Untitled](Documentacio%CC%81n%20Sistema%20de%20Misiones%20369793a943eb4f1e9499365ce9c20f44/Untitled%2010.png)
    
    Este script es el controlador del panel (menú) Panel-QuestTracker, teniendo ya una UI diseñada pero cuyos elementos pueden reorganizarse si así se desea.
    
    ![Untitled](Documentacio%CC%81n%20Sistema%20de%20Misiones%20369793a943eb4f1e9499365ce9c20f44/Untitled%2011.png)
    

- **Prefabs**
    
    Habrás visto que se han agregado nuevos prefabs a tu carpeta (o de no contar con una se habrá creado). 
    
    ![Untitled](Documentacio%CC%81n%20Sistema%20de%20Misiones%20369793a943eb4f1e9499365ce9c20f44/Untitled%2012.png)
    
    El primer prefab **Button**, es simplemente un boton asignado al panel identificador de misiones.
    
    El prefab llamado **Destino** es un empty que deberá ser colocado en el lugar del mapa que se requiera que el jugador llegue al momento de una asignarse una y si esta lo requiere. 
    
    El siguiente Prefab, nombrado **Panel-Misión** es el menú a través del que tu personaje aceptará sus misiones.
    
    Finalmente, el prefab nombrado **Panel-QuestTracker**, es el prefab del menú para revisar el estado de misiones en el juego.
    

- **Scripts y partes Modulares**
    
    Para finalizar, en la carpeta **SCRIPTSMODULARES** encontrarás scripts que probablemente ya tengas o planees tener en tu juego; vienen de esta manera pues dentro solo llevas unas pequeñas líneas de código como identificador en los demás scripts, por lo que, puedes simplemente copiar y pegar en tus propios scripts y disponer de ellos.
    
    ![Untitled](Documentacio%CC%81n%20Sistema%20de%20Misiones%20369793a943eb4f1e9499365ce9c20f44/Untitled%2013.png)