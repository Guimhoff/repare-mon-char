using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableCreation : MonoBehaviour
{
    public GameObject[] cableLinks; // Tableau de GameObjects représentant les maillons du câble
    public GameObject startNode; // GameObject représentant le début du câble
    public GameObject endNode; // GameObject représentant la fin du câble

    public float springForce = 1f; // Force du ressort
    public float distance = 0.25f;

    public int numberOfLinks; // Nombre de maillons du câble

    public float lineWidth = 0.1f; // Épaisseur du câble
    public Color lineColor = Color.white; // Couleur du câble

    public Mesh sphereMesh; // Mesh de la sphère à utiliser

    public float linkMass = 1f; // Masse des maillons du câble

    private LineRenderer[] lineRenderers; // Tableau des Line Renderers

    // Start is called before the first frame update
    void Start()
    {
        // Créer les maillons du câble
        CreateCable();
    }

    // Update is called once per frame
    void Update()
    {
        // Actualiser les positions des Line Renderers à chaque frame
        UpdateLineRendererPositions();
    }

    // Fonction pour créer le câble
    void CreateCable()
    {
        // Vérifier si le nombre de maillons est valide
        if (numberOfLinks < 2)
        {
            Debug.LogWarning("Le nombre de maillons doit être au moins de 2.");
            return;
        }

        // Créer le tableau de GameObjects pour les maillons du câble
        cableLinks = new GameObject[numberOfLinks];

        // Créer le premier maillon attaché au début du câble
        cableLinks[0] = startNode;

        // Créer les maillons intermédiaires
        for (int i = 1; i < numberOfLinks - 1; i++)
        {
            GameObject link = new GameObject("CableLink_" + i); // Créer un GameObject pour le maillon

            // Ajouter un MeshFilter et assigner le mesh de la sphère
            MeshFilter meshFilter = link.AddComponent<MeshFilter>();
            meshFilter.mesh = sphereMesh; // Assigner le mesh de la sphère fourni

            // Ajouter un MeshRenderer pour le rendu de la sphère
            MeshRenderer meshRenderer = link.AddComponent<MeshRenderer>();
            meshRenderer.material.color = lineColor; // Appliquer la couleur spécifiée

            Rigidbody linkRigidbody = link.AddComponent<Rigidbody>(); // Ajouter un composant Rigidbody pour la physique
            linkRigidbody.mass = linkMass; // Ajout de sa masse

            SphereCollider collider = link.AddComponent<SphereCollider>(); // Ajouter un composant Collider
            collider.radius = 1 / 2f; // Définir le rayon du collider selon l'épaisseur du câble

            // Mettre à l'échelle la sphère pour correspondre à l'épaisseur du câble
            link.transform.localScale = new Vector3(lineWidth, lineWidth, lineWidth);

            link.transform.position = Vector3.Lerp(startNode.transform.position, endNode.transform.position, (float)i / (numberOfLinks - 1)); // Positionner le maillon le long du câble
            cableLinks[i] = link; // Ajouter le maillon au tableau
        }

        // Créer le dernier maillon attaché à la fin du câble
        cableLinks[numberOfLinks - 1] = endNode;

        for (int i = 1; i < numberOfLinks - 1; i++)
        {
            // Ajouter et configurer le script CableLinkLogic
            CableLinkLogic cableLinkLogic = cableLinks[i].AddComponent<CableLinkLogic>();
            cableLinkLogic.objectA = i > 0 ? cableLinks[i - 1] : null; // L'objet A est le maillon précédent, sauf pour le premier maillon
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
        endNodeLogic.objectA = cableLinks[numberOfLinks - 2]; // L'objet A du dernier maillon est le maillon précédent
        endNodeLogic.objectB = null; // L'objet B du dernier maillon est null
        endNodeLogic.springForce = springForce;
        endNodeLogic.distance = distance;

        // Ajouter les Line Renderers pour tracer le câble
        lineRenderers = new LineRenderer[numberOfLinks - 1]; // Créer le tableau de Line Renderers
        for (int i = 0; i < numberOfLinks - 1; i++)
        {
            GameObject link = cableLinks[i];
            LineRenderer lineRenderer = link.AddComponent<LineRenderer>(); // Ajouter un Line Renderer
            lineRenderer.positionCount = 2; // Deux points pour chaque segment de câble
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.startColor = lineColor;
            lineRenderer.endColor = lineColor;
            lineRenderer.SetPosition(0, link.transform.position); // Position du début du segment
            lineRenderer.SetPosition(1, cableLinks[i + 1].transform.position); // Position de fin du segment
            lineRenderers[i] = lineRenderer; // Ajouter le Line Renderer au tableau
        }
    }

    // Fonction pour actualiser les positions des Line Renderers
    void UpdateLineRendererPositions()
    {
        // Parcourir tous les Line Renderers et mettre à jour leurs positions
        for (int i = 0; i < lineRenderers.Length; i++)
        {
            lineRenderers[i].SetPosition(0, cableLinks[i].transform.position); // Position du début du segment
            lineRenderers[i].SetPosition(1, cableLinks[i + 1].transform.position); // Position de fin du segment
        }
    }
}
