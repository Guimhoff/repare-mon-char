using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableCreation : MonoBehaviour
{
    public GameObject[] cableLinks; // Tableau de GameObjects repr�sentant les maillons du c�ble
    public GameObject startNode; // GameObject repr�sentant le d�but du c�ble
    public GameObject endNode; // GameObject repr�sentant la fin du c�ble

    public float springForce = 1f; // Force du ressort
    public float distance = 0.25f;

    public int numberOfLinks; // Nombre de maillons du c�ble

    public float lineWidth = 0.1f; // �paisseur du c�ble
    public Color lineColor = Color.white; // Couleur du c�ble

    public Mesh sphereMesh; // Mesh de la sph�re � utiliser

    public float linkMass = 1f; // Masse des maillons du c�ble

    private LineRenderer[] lineRenderers; // Tableau des Line Renderers

    // Start is called before the first frame update
    void Start()
    {
        // Cr�er les maillons du c�ble
        CreateCable();
    }

    // Update is called once per frame
    void Update()
    {
        // Actualiser les positions des Line Renderers � chaque frame
        UpdateLineRendererPositions();
    }

    // Fonction pour cr�er le c�ble
    void CreateCable()
    {
        // V�rifier si le nombre de maillons est valide
        if (numberOfLinks < 2)
        {
            Debug.LogWarning("Le nombre de maillons doit �tre au moins de 2.");
            return;
        }

        // Cr�er le tableau de GameObjects pour les maillons du c�ble
        cableLinks = new GameObject[numberOfLinks];

        // Cr�er le premier maillon attach� au d�but du c�ble
        cableLinks[0] = startNode;

        // Cr�er les maillons interm�diaires
        for (int i = 1; i < numberOfLinks - 1; i++)
        {
            GameObject link = new GameObject("CableLink_" + i); // Cr�er un GameObject pour le maillon

            // Ajouter un MeshFilter et assigner le mesh de la sph�re
            MeshFilter meshFilter = link.AddComponent<MeshFilter>();
            meshFilter.mesh = sphereMesh; // Assigner le mesh de la sph�re fourni

            // Ajouter un MeshRenderer pour le rendu de la sph�re
            MeshRenderer meshRenderer = link.AddComponent<MeshRenderer>();
            meshRenderer.material.color = lineColor; // Appliquer la couleur sp�cifi�e

            Rigidbody linkRigidbody = link.AddComponent<Rigidbody>(); // Ajouter un composant Rigidbody pour la physique
            linkRigidbody.mass = linkMass; // Ajout de sa masse

            SphereCollider collider = link.AddComponent<SphereCollider>(); // Ajouter un composant Collider
            collider.radius = 1 / 2f; // D�finir le rayon du collider selon l'�paisseur du c�ble

            // Mettre � l'�chelle la sph�re pour correspondre � l'�paisseur du c�ble
            link.transform.localScale = new Vector3(lineWidth, lineWidth, lineWidth);

            link.transform.position = Vector3.Lerp(startNode.transform.position, endNode.transform.position, (float)i / (numberOfLinks - 1)); // Positionner le maillon le long du c�ble
            cableLinks[i] = link; // Ajouter le maillon au tableau
        }

        // Cr�er le dernier maillon attach� � la fin du c�ble
        cableLinks[numberOfLinks - 1] = endNode;

        for (int i = 1; i < numberOfLinks - 1; i++)
        {
            // Ajouter et configurer le script CableLinkLogic
            CableLinkLogic cableLinkLogic = cableLinks[i].AddComponent<CableLinkLogic>();
            cableLinkLogic.objectA = i > 0 ? cableLinks[i - 1] : null; // L'objet A est le maillon pr�c�dent, sauf pour le premier maillon
            cableLinkLogic.objectB = i < numberOfLinks - 1 ? cableLinks[i + 1] : null; // L'objet B est le maillon suivant, sauf pour le dernier maillon
            cableLinkLogic.springForce = springForce;
            cableLinkLogic.distance = distance;
        }

        // Ajouter et configurer le script CableLinkLogic pour le premier maillon (startNode)
        CableLinkLogic startNodeLogic = startNode.AddComponent<CableLinkLogic>();
        startNodeLogic.objectA = null; // L'objet B du dernier maillon est null
        startNodeLogic.objectB = cableLinks[1]; // L'objet A du premier maillon est le maillon suivant
        startNodeLogic.springForce = springForce;
        startNodeLogic.distance = distance;

        // Ajouter et configurer le script CableLinkLogic pour le dernier maillon (endNode)
        CableLinkLogic endNodeLogic = endNode.AddComponent<CableLinkLogic>();
        endNodeLogic.objectA = cableLinks[numberOfLinks - 2]; // L'objet A du dernier maillon est le maillon pr�c�dent
        endNodeLogic.objectB = null; // L'objet B du dernier maillon est null
        endNodeLogic.springForce = springForce;
        endNodeLogic.distance = distance;

        // Ajouter les Line Renderers pour tracer le c�ble
        lineRenderers = new LineRenderer[numberOfLinks - 1]; // Cr�er le tableau de Line Renderers
        for (int i = 0; i < numberOfLinks - 1; i++)
        {
            GameObject link = cableLinks[i];
            LineRenderer lineRenderer = link.AddComponent<LineRenderer>(); // Ajouter un Line Renderer
            lineRenderer.positionCount = 2; // Deux points pour chaque segment de c�ble
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.startColor = lineColor;
            lineRenderer.endColor = lineColor;
            lineRenderer.SetPosition(0, link.transform.position); // Position du d�but du segment
            lineRenderer.SetPosition(1, cableLinks[i + 1].transform.position); // Position de fin du segment
            lineRenderers[i] = lineRenderer; // Ajouter le Line Renderer au tableau
        }
    }

    // Fonction pour actualiser les positions des Line Renderers
    void UpdateLineRendererPositions()
    {
        // Parcourir tous les Line Renderers et mettre � jour leurs positions
        for (int i = 0; i < lineRenderers.Length; i++)
        {
            lineRenderers[i].SetPosition(0, cableLinks[i].transform.position); // Position du d�but du segment
            lineRenderers[i].SetPosition(1, cableLinks[i + 1].transform.position); // Position de fin du segment
        }
    }
}
